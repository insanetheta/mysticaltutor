__author__ = 'greg'

from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Card, CardSet, Product
import requests
import sys
from django.db import models
from dealfinder import productFetcher


class Command(BaseCommand):
	TCG_PARTNER_KEY = 'DEALFINDER'

	def handle(self, *args, **options):
		cardSets = CardSet.objects.all()
		depthcount = 15
		for set in cardSets:
			if (depthcount < 1):
				break
			print(unicode(set.name).encode('utf-8'))
			cardIds = set.getCardIds()
			for cardId in cardIds:
				card = None
				print(cardId)
				try:
					card = Card.objects.get(multiverseId=cardId)
				except:
					#print("Error: ", sys.exc_info()[0])
					#raise
					print('Card ' + cardId + ' Does not Exist')
				if (card != None):
					print((set.name + ' : ' + card.name).encode('utf-8'))
					req = requests.get(self.generateTcgUrl(set.name, card.name))
					product = productFetcher.productFromXmlString(req.text, card.name)
					if (product != None):
						try:
							print("Product Exists! Updating with latest")
							print("tcgId: " + product.tcgId)
							productTcgId = product.tcgId
							existingProduct = Product.objects.get(tcgId=product.tcgId)
							print("GotHere1!")
							existingProduct.updateFieldsFromProduct(product)
							existingProduct.save()
							print("GotHere2!")
							if (card.product == None):
								card.product = existingProduct
								card.save()
						except:
							# print "Unexpected error:", sys.exc_info()[0]
							#raise
							print("No Product Exists! Updating Product")
							product.save()
							card.product = product
							card.save()
					else:
						print("The Product Was missing from TCG Player or was malformed")
			depthcount -= 1

	def generateTcgUrl(self, setName, cardName):
		requestString = 'http://partner.tcgplayer.com/x3/phl.asmx/p?pk=' + self.TCG_PARTNER_KEY + '&s=' + setName + '&p=' + cardName
		return requestString
