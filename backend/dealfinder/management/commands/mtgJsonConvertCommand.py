from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Product
from dealfinder.models import CardSet
from dealfinder.models import Card
from dealfinder import productFetcher
from django.core.exceptions import ValidationError
from django.db import models
import django.core.exceptions
from dealfinder.settings import PROJECT_ROOT
import requests
import json
import os


class Command(BaseCommand):
	help = 'Fetch Cards for one Set'

	def add_arguments(self, parser):
		parser.add_argument('cardSetId', nargs='+', type=string)

	def handle(self, *args, **options):
		allCardsOutputString = open(os.path.join(PROJECT_ROOT, '../../jsoncarddata/AllSets-x.json')).read()
		setsDict = json.loads(allCardsOutputString)

		cardUpdatedCount = 0
		setUpdatedCount = 0
		for setKey in setsDict:
			setUpdatedCount += 1
			setDict = setsDict[setKey]
			set = None
			try:
				set = CardSet.objects.get(setCode=setDict['code'])
				newSet = CardSet.create(setDict)
				set.updateFieldsFromSet(newSet)
			except CardSet.DoesNotExist:
				set = CardSet.create(setDict)
				set.save()
			print(set.name)

			for cardDict in setDict['cards']:
				cardUpdatedCount += 1
				product = self.getProductFromName(cardDict['name'])
				try:
					print(cardDict['name'])
				except:
					print('Couldnt Print Name')
				try:
					mvId = cardDict.get('multiverseid', None)
					if(mvId != None):
						card = Card.objects.get(multiverseId=mvId)
						newCard = Card.create(cardDict, product)
						card.updateFieldsFromCard(newCard)
						card.save()
					else:
						print('No multiverseId!')
				except Card.DoesNotExist:
					card = Card.create(cardDict, product)
					#print('Create New Card: ' + card.name + 'mvId' + mvId)
					try:
						card.save()
					except django.db.utils.IntegrityError:
						print('Product ID Duplicate!')
		print('Sets: ' + str(setUpdatedCount) + ' , Cards: ' + str(cardUpdatedCount))

	def getProductFromName(self, name):
		product = None
		try:
			product = Product.objects.get(productName=name)
		except:
			product = None
		return product