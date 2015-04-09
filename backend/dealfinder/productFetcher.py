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
	print tcgXml
	tcgDictList = []
	for child in tcgXml:
		print child
		tcgDictList.append(xmldict.xml_to_dict(child))
	print tcgDictList[1]
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
	print productList[0].tcgId
	return productList
	
	
#parseProductXmlAndStore()