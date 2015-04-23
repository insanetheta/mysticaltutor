from django.contrib import admin
from dealfinder.models import Product
from dealfinder.models import CardSet
from dealfinder.models import Card

class ProductAdmin(admin.ModelAdmin):
	# fieldsets = [
		# (None, {'fields': ['productName']}),
	# ]

	list_display = ('productName', 'lowPrice')

class CardSetAdmin(admin.ModelAdmin):
	list_display = ('name', 'setCode')
	
class CardAdmin(admin.ModelAdmin):
	list_display = ('name', 'multiverseId')
	
admin.site.register(Product, ProductAdmin)
admin.site.register(CardSet, CardSetAdmin)
admin.site.register(Card, CardAdmin)
