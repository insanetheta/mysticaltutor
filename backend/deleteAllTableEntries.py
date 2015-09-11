from dealfinder.models import Product

while Product.objects.count():
    ids = Product.objects.values_list('pk', flat=True)[:100]
    Product.objects.filter(pk__in = ids).delete()