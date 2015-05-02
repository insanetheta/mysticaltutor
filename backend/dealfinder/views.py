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
	return JsonResponse(getCardProductDictList(card_list), safe=False)

def getCardProductDictList(card_list):
	card_dict_list = []
	for card in card_list:
		card_dict = {}
		card_dict['name'] = card.name
		card_dict['multiverseId'] = card.multiverseId
		card_dict['rarity'] = card.rarity
		mtgFormats = []
		for mtgFormat in card.formats.split(","):
			mtgFormats.append(mtgFormat.strip("'" '"' ' '))
		mtgFormats = filter(isRelevantFormat, mtgFormats)
		#formats.append(card.formats[0])
		card_dict['formats'] = mtgFormats
		#card_dict['formats'] = filter(lambda x: isRelevantFormat(x), card.formats)
		card_dict['hiPrice'] = card.product.hiPrice
		card_dict['lowPrice'] = card.product.lowPrice
		card_dict['avgPrice'] = card.product.avgPrice
		card_dict['link'] = card.product.link
		card_dict_list.append(card_dict)
	return card_dict_list

def getCardDictList(card_list):
	card_dict_list = []
	for card in card_list:
		card_dict = {}
		card_dict['name'] = card.name
		card_dict['multiverseId'] = card.multiverseId
		card_dict['rarity'] = card.rarity
		formats = []
		#for format in card.formats:
		#	formats.append(format)
		#formats = filter(lambda x: isRelevantFormat(x), card.formats)
		#formats.append(card.formats[0])
		card_formats = formats
		card_dict['formats'] = card_formats#str(card_formats).strip('[]')
		#card_dict['formats'] = card.formats
		card_dict_list.append(card_dict)
	return card_dict_list

def isRelevantFormat(format):
	if format.lower() == 'standard'.lower():
		return True
	if format.lower() == 'modern'.lower():
		return True
	if format.lower() == 'legacy'.lower():
		return True
	return False


def formatFilterApi(request, format_name):
	card_list = Card.objects.exclude(product=None).order_by('name')
	card_list = filter(lambda x: formatFilter(x, format_name), card_list)
	return JsonResponse(getCardProductDictList(card_list), safe=False)

def formatFilter(card, format_name):
	print(format_name)
	in_format = format_name in card.formats
	return in_format

def singleCardApi(request, card_name):
	card_list = Card.objects.exclude(product=None).filter(name__iexact=card_name)
	return JsonResponse(getCardProductDictList((card_list)), safe=False)

def allCardsApi(request):
	card_list = Card.objects.all()
	return JsonResponse(getCardDictList(card_list), safe=False)