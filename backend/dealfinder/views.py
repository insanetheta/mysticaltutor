from django.http import HttpResponse
from django.http import JsonResponse
from django.template import RequestContext, loader
from django.http import Http404
from django.shortcuts import render

from dealfinder.models import Product
from dealfinder.models import Card

def index(request):
	card_list = Card.objects.exclude(product=None).order_by('name')
	return render(request, 'dealfinder/index.html', {'card_list': card_list})
	
#def productDetail(request):
#	return render(request, 'dealfinder/detail.html')

def cardDetail(request, card_id):
	try:
		card = Card.objects.get(multiverseId=card_id)
	except Card.DoesNotExist:
		raise Http404("Card does not exist")
	return render(request, 'cardDetail.html', {'card': card})

def productDetail(request, product_id):
	try:
		product = Product.objects.get(pk=product_id)
	except Product.DoesNotExist:
		raise Http404("Product does not exist")
	return render(request, 'detail.html', {'product': product})
	
def mobileApi(request):
	card_list = Card.objects.exclude(product=None).order_by('name')
	card_dict_list = []
	for card in card_list:
		card_dict = {}
		card_dict['name'] = card.name
		card_dict['cardSetName'] = card.cardSetName
		card_dict['rarity'] = card.rarity
		card_dict['formats'] = card.formats
		card_dict['hiPrice'] = card.product.hiPrice
		card_dict['lowPrice'] = card.product.lowPrice
		card_dict['avgPrice'] = card.product.avgPrice
		card_dict['link'] = card.product.link
		card_dict_list.append(card_dict)
		
	return JsonResponse(card_dict_list, safe=False)