import xmldict
import os
import xml.etree.ElementTree as ET
from settings import PROJECT_ROOT
from django.db import models
from dealfinder.models import Product

def parseProductXmlAndStore():
	tcgOutputFile = open(os.path.join(PROJECT_ROOT, 'tcgoutput.xml'))
	#print(tcgOutputFile)
	#print(tcgOutputFile.read())
	tcgOutputString = tcgOutputFile.read();
	#print(tcgOutputString)
	tcgXml = ET.fromstring(tcgOutputString)
	#print tcgXml
	tcgDictList = []
	for child in tcgXml:
		#print child
		tcgDictList.append(xmldict.xml_to_dict(child))
	#print tcgDictList[1]
	# tcgOutputDict = xmldict.xml_to_dict(tcgOutputString)
	# print(tcgOutputDict)
	# productArray = tcgOutputDict['products']['product']
	# print(productArray)
	# for product in productArray:
		# print product['id']
	productList = []
	for tcgDict in tcgDictList:
		prod = Product.create(tcgDict['product'])
		productList.append(prod)
	#print productList[0].tcgId
	return productList

# takes a single product xml and creates a db Product Model
def productFromXmlString(tcgXmlString, cardName):
	tcgXml = None
	try:
		tcgXml = ET.fromstring(tcgXmlString)
	except:
		print('tcgXml Failed to Parse: ' + tcgXmlString)
	if(None != tcgXml):
		tcgDictList = []
		for child in tcgXml:
			tcgDictList.append(xmldict.xml_to_dict(child))
		tcgDict = get_first(tcgDictList)
		product = None
		if(None != tcgDict):
			productDict = tcgDict['product']
			productDict['name'] = cardName
			print('tcgProduct: ' + str(productDict))
			product = Product.create(productDict)
		return product
	else:
		return None

def get_first(iterable, default=None):
    if iterable:
        for item in iterable:
            return item
    return default
	
	
#parseProductXmlAndStore()