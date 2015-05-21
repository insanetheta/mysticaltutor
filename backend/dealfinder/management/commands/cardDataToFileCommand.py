from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Product
from dealfinder.models import CardSet
from dealfinder.models import Card
from dealfinder import productFetcher
from django.core.exceptions import ValidationError
from django.db import models
import requests
import json
from dealfinder.settings import PROJECT_ROOT
from dealfinder import views
import os, sys


class Command(BaseCommand):
    help = 'Fetch Cards for one Set'

    def add_arguments(self, parser):
        parser.add_argument('cardSetId', nargs='+', type=string)

    def handle(self, *args, **options):
		#print("handle")
		cardDictList = views.getCardDictList(Card.objects.all())
		cardDictJsonString = json.dumps(cardDictList)
		# Open file
		#fd = os.open("f1.txt",os.O_RDWR|os.CREAT)
		cardDictFile = os.open(os.path.join(PROJECT_ROOT, 'static/card_data/card_data_base.json'), os.O_RDWR|os.O_CREAT)
		ret = os.write(cardDictFile, cardDictJsonString)
		print ret
		os.close(cardDictFile)
		print "Written Successfully!"