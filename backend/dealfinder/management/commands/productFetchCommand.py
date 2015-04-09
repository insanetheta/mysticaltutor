from django.core.management.base import BaseCommand, CommandError
from dealfinder.models import Product
from dealfinder import productFetcher
from django.core.exceptions import ValidationError
from django.db import models


class Command(BaseCommand):
    help = 'Fetch Products from TcgPlayer'

    def add_arguments(self, parser):
        parser.add_argument('poll_id', nargs='+', type=int)

    def handle(self, *args, **options):
        # for poll_id in options['poll_id']:
            # try:
                # product = Product.objects.get(pk=poll_id)
            # except Product.DoesNotExist:
                # raise CommandError('Product "%s" does not exist' % poll_id)

            # product.opened = False
            # product.save()

            # self.stdout.write('Successfully closed product "%s"' % poll_id)
		prodList = productFetcher.parseProductXmlAndStore()
		for product in prodList:
			try:
				existingProduct = Product.objects.get(tcgId=product.tcgId)
				existingProduct.updateFieldsFromProduct(product)
				existingProduct.clean()
				existingProduct.save()
				print('Updated Product: ' + existingProduct.productName)
			except Product.DoesNotExist:
				product.clean()
				product.save()
				print('Added New Product: ' + product.productName)
				pass
		
		print('Updated ' + str(len(prodList)) + ' Entries')