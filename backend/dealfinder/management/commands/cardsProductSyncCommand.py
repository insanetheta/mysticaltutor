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
		cards = Card.objects.all()
		for card in cards:
			product = None
			try:
				product = Product.objects.get(productName=card.name)
			except:
				product = None
			if(None != product):
				card.product = product
				card.save()
				print(card.name + ' linked!')