from django.conf.urls import patterns, url, include
from django.contrib import admin

from dealfinder import views

urlpatterns = patterns('',
	#ex dealfinder
	url(r'^$', views.index, name='index'),
	#ex: /dealfinder/5/
	url(r'^dealfinder/product/(?P<product_id>\d+)/$', views.productDetail, name='productDetail'),
	url(r'^dealfinder/card/(?P<card_id>\d+)/$', views.cardDetail, name='cardDetail'),
	url (r'^mobile/api/$', views.mobileApi, name='mobileApi'),
	url(r'^mobile/api/format\=(?P<format_name>.*)/$', views.formatFilterApi, name='formatFilterApi'),
	#url(r'^dealfinder/', include('dealfinder.urls', namespace="dealfinder")),
	url(r'^polls/', include('polls.urls', namespace="polls")),
    url(r'^admin/', include(admin.site.urls)),
)