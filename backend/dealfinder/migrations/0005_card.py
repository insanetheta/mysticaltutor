# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0004_cardset'),
    ]

    operations = [
        migrations.CreateModel(
            name='Card',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('multiverseId', models.CharField(default=b'none', max_length=200)),
                ('name', models.CharField(default=b'none', max_length=200)),
                ('searchName', models.CharField(default=b'none', max_length=200)),
                ('colors', models.CharField(default=b'', max_length=200)),
                ('cardSetName', models.CharField(default=b'none', max_length=200)),
                ('rarity', models.CharField(default=b'none', max_length=200)),
                ('cardSetId', models.CharField(default=b'none', max_length=200)),
                ('token', models.BooleanField(default=False)),
                ('promo', models.BooleanField(default=False)),
                ('formats', models.CharField(default=b'', max_length=200)),
            ],
            options={
            },
            bases=(models.Model,),
        ),
    ]
