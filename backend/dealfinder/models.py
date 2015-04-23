from django.db import models
from django.utils import encoding

class Product(models.Model):
	productName = models.CharField(default='none', max_length=200)
	tcgId = models.IntegerField()
	hiPrice = models.FloatField()
	lowPrice = models.FloatField()
	avgPrice = models.FloatField()
	link = models.TextField(max_length=200)
	
	def updateFieldsFromProduct(self, productInst):
		self.productName = productInst.productName
		self.tcgId = productInst.tcgId
		self.hiPrice = productInst.hiPrice
		self.lowPrice = productInst.lowPrice
		self.avgPrice = productInst.avgPrice
		self.link = productInst.link
		return self
	
	@classmethod
	def create(cls, jsonProductObject):
		product = cls()
		product.productName = jsonProductObject['name']
		product.tcgId = jsonProductObject.get('id', -1)
		product.hiPrice = jsonProductObject['hiprice']
		product.avgPrice = jsonProductObject['avgprice']
		product.lowPrice = jsonProductObject['lowprice']
		product.link = jsonProductObject['link']
		return product
		
class CardSet(models.Model):
	setCode = models.CharField(default='none', max_length=200, unique=True)
	gathererCode = models.CharField(default='none', max_length=200)
	name = models.CharField(default='none', max_length=200)
	type = models.CharField(default='none', max_length=200)
	block = models.CharField(default='none', max_length=200)
	total = models.IntegerField(default=0)
	cardIds = models.CharField(default='', max_length=500) #array of str    
	
	def updateFieldsFromSet(self, setInst):
		self.setCode = setInst.setCode
		self.gathererCode = setInst.gathererCode
		self.name = setInst.name
		self.type = setInst.type
		self.block = setInst.block
		self.cardIds = setInst.cardIds
		return self
	
	@classmethod
	def create(cls, jsonCardSetObject):
		cardSet = cls()
		cardSet.setCode = jsonCardSetObject['code']
		print(cardSet.setCode)
		cardSet.gathererCode = jsonCardSetObject.get('gathererCode', 'none')
		cardSet.name = jsonCardSetObject['name']
		cardSet.type = jsonCardSetObject['type']
		cardSet.block = jsonCardSetObject.get('block', 'none')
		cards = []
		for card in jsonCardSetObject['cards']:
			# try:
				# print(encoding.smart_text(card['name'], encoding='utf-8'))
			# except:
				# print('cannot encode card name')
			if(card.get('multiverseid', None) != None):
				cards.append(card['multiverseid'])
		cardSet.cardIds = str(cards).strip('[]')
		return cardSet
		
class Card(models.Model):
	multiverseId = models.CharField(default='none', max_length=200, unique=True)
	name = models.CharField(default='none', max_length=200)
	colors = models.CharField(default='', max_length=200) #array of str
	rarity = models.CharField(default='none', max_length=200)
	formats = models.CharField(default='', max_length=200) #array of str
	product = models.OneToOneField(Product, blank=True, null=True)
	
	def updateFieldsFromCard(self, cardInst):
		multiverseId = cardInst.multiverseId
		name = cardInst.name
		colors = cardInst.colors
		rarity = cardInst.rarity
		formats = cardInst.formats
		product = cardInst.product
		return self
	
	@classmethod
	def create(cls, jsonCardObject, product):
		card = cls()
		card.multiverseId = jsonCardObject.get('multiverseid', '-1')
		card.name = jsonCardObject['name']
		card.colors = str(jsonCardObject.get('colors', '')).strip('[]')
		#print(str(card.colors))
		card.rarity = jsonCardObject['rarity']
		legalities = jsonCardObject.get('legalities','')
		formats = []
		for legalityKey in legalities:
			formats.append(str(legalityKey))
		#print('cardName: ' + card.name + ' , formats: ' + str(len(formats)))
		card.formats = str(formats).strip('[]')
		if(product != None):
			card.product = product
		return card
	
	