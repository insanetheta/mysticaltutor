from django.db import models

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
		product.productName = "Name"
		product.tcgId = jsonProductObject['id']
		product.hiPrice = jsonProductObject['hiprice']
		product.avgPrice = jsonProductObject['avgprice']
		product.lowPrice = jsonProductObject['lowprice']
		product.link = jsonProductObject['link']
		return product
		
class CardSet(models.Model):
	setId = models.CharField(default='none', max_length=200)
	name = models.CharField(default='none', max_length=200)
	type = models.CharField(default='none', max_length=200)
	block = models.CharField(default='none', max_length=200)
	total = models.IntegerField(default=0)
	cardIds = models.CharField(default='', max_length=500) #array of str    
	
	def updateFieldsFromSet(self, setInst):
		self.setId = setInst.setId
		self.name = setInst.name
		self.type = setInst.type
		self.block = setInst.block
		self.total = setInst.total
		self.cardIds = setInst.cardIds
		return self
	
	@classmethod
	def create(cls, jsonCardSetObject):
		cardSet = cls()
		cardSet.setId = jsonCardSetObject['id']
		cardSet.name = jsonCardSetObject['name']
		cardSet.type = jsonCardSetObject['type']
		if(jsonCardSetObject['block'] != None):
			cardSet.block = jsonCardSetObject['block']
		else:
			cardSet.block = 'none'
		cardSet.total = jsonCardSetObject['total']
		cardSet.cardIds = str(jsonCardSetObject['cardIds']).strip('[]')
		return cardSet
		
class Card(models.Model):
	multiverseId = models.CharField(default='none', max_length=200)
	name = models.CharField(default='none', max_length=200)
	searchName = models.CharField(default='none', max_length=200)
	colors = models.CharField(default='', max_length=200) #array of str
	cardSetName = models.CharField(default='none', max_length=200)
	rarity = models.CharField(default='none', max_length=200)
	cardSetId = models.CharField(default='none', max_length=200)
	token = models.BooleanField(default=False)
	promo = models.BooleanField(default=False)
	formats = models.CharField(default='', max_length=200) #array of str
	product = models.OneToOneField(Product, blank=True, null=True)
	
	def updateFieldsFromCard(self, cardInst):
		multiverseId = cardInst.multiverseId
		name = cardInst.name
		searchName = cardInst.searchName
		colors = cardInst.colors
		cardSetName = cardInst.cardSetName
		rarity = cardInst.rarity
		cardSetId = cardInst.cardSetId
		token = cardInst.token
		promo = cardInst.promo
		formats = cardInst.formats
		product = cardInst.product
		return self
	
	@classmethod
	def create(cls, jsonCardObject, product):
		card = cls()
		card.multiverseId = jsonCardObject['id']
		card.name = jsonCardObject['name']
		card.searchName = jsonCardObject['searchName']
		card.colors = str(jsonCardObject['colors']).strip('[]')
		card.cardSetName = jsonCardObject['cardSetName']
		card.rarity = jsonCardObject['rarity']
		card.cardSetId = jsonCardObject['cardSetId']
		card.token = jsonCardObject['token']
		card.promo = jsonCardObject['promo']
		card.formats = str(jsonCardObject['formats']).strip('[]')
		if(product != None):
			card.product = product
		return card
	
	