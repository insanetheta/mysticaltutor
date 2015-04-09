from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Product
from dealfinder.models import CardSet
from dealfinder.models import Card
from dealfinder import productFetcher
from django.core.exceptions import ValidationError
from django.db import models
import requests
import json


class Command(BaseCommand):
    help = 'Fetch Cards for one Set'

    def add_arguments(self, parser):
        parser.add_argument('cardSetId', nargs='+', type=string)

    def handle(self, *args, **options):
		#print("handle")
		print(args[0])
		r = requests.get(str('http://api.mtgdb.info/sets/' + str(args[0]) + '/cards'))
		cardsDict = json.loads(r.text)
		#print(cardsDict)
		for cardDict in cardsDict:
			product = None
			try:
				product = Product.objects.get(productName=cardDict['name'])
				print(card.name)
				print(product.link)
			except:
				product = None
			card = Card.create(cardDict, product)
			try:
				existingCard = Card.objects.get(multiverseId=card.multiverseId)
				existingCard.updateFieldsFromCard(card)
				existingCard.clean()
				existingCard.save()
				print('Updated Card: ' + existingCard.name)
			except Card.DoesNotExist:
				card.clean()
				card.save()
				print('Added new card: ' + card.name)