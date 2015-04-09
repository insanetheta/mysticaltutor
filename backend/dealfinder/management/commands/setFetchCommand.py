from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Product
from dealfinder.models import CardSet
from dealfinder import productFetcher
from django.core.exceptions import ValidationError
from django.db import models
import requests
import json


class Command(BaseCommand):
    help = 'Fetch Products from TcgPlayer'

    def add_arguments(self, parser):
        parser.add_argument('poll_id', nargs='+', type=int)

    def handle(self, *args, **options):
		#print("handle")
		r = requests.get('http://api.mtgdb.info/sets')
		#print(r.text)
		setsDict = json.loads(r.text)
		for setDict in setsDict:
			cardSet = CardSet.create(setDict)
			print(cardSet.name)
			try:
				existingSet = CardSet.objects.get(setId=cardSet.setId)
				existingSet.updateFieldsFromSet(cardSet)
				existingSet.clean()
				existingSet.save()
				print("Updated Set: " + existingSet.name)
			except CardSet.DoesNotExist:
				cardSet.clean()
				cardSet.save()
				print('Added new set: ' + cardSet.name)
			