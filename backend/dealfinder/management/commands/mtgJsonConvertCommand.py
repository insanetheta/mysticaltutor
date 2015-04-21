from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Product
from dealfinder.models import CardSet
from dealfinder.models import Card
from dealfinder import productFetcher
from django.core.exceptions import ValidationError
from django.db import models
from dealfinder.settings import PROJECT_ROOT
import requests
import json
import os


class Command(BaseCommand):
    help = 'Fetch Cards for one Set'

    def add_arguments(self, parser):
        parser.add_argument('cardSetId', nargs='+', type=string)

    def handle(self, *args, **options):
        #print(args[0])
		allCardsOutputString = open(os.path.join(PROJECT_ROOT, '../../jsoncarddata/AllSets-x.json')).read()
		setsDict = json.loads(allCardsOutputString)
		#print(cardsDict)
		cardUpdatedCount = 0
		setUpdatedCount = 0
		for setKey in setsDict:
			setUpdatedCount += 1
			setDict = setsDict[setKey]
			set = None
			try:
				set = CardSet.objects.get(setId=setDict['code'])
				set.updateFieldsFromSet(setDict)
			except:
				set = CardSet.create(setDict)
				set.save()
			print(set)
			
			#print(setsDict[setKey]['name'])

			for cardDict in setDict['cards']:
				cardUpdatedCount += 1
				product = self.getProductFromName(cardDict['name'])
				try:
					card = Card.objects.get(multiverseId=cardDict['multiverseid'])
					newCard = Card.create(cardDict,product)
					card.updateFieldsFromCard(cardDict)
					card.save()
				except:
					card = Card.create(cardDict, product)
					card.save()
			print('Sets: ' + str(setUpdatedCount) + ' , Cards: ' + str(cardUpdatedCount))

	def getProductFromName(self, name):
		product = None
		try:
			product = Product.objects.get(productName=name)
		except:
			product = None
		return product