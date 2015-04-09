# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Product',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('tcgId', models.IntegerField()),
                ('hiPrice', models.FloatField()),
                ('lowPrice', models.FloatField()),
                ('avgPrice', models.FloatField()),
                ('link', models.TextField()),
            ],
            options={
            },
            bases=(models.Model,),
        ),
    ]
