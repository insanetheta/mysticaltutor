# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0007_auto_20150415_1939'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='card',
            name='cardSetId',
        ),
        migrations.RemoveField(
            model_name='card',
            name='cardSetName',
        ),
        migrations.RemoveField(
            model_name='card',
            name='promo',
        ),
        migrations.RemoveField(
            model_name='card',
            name='searchName',
        ),
        migrations.RemoveField(
            model_name='card',
            name='token',
        ),
    ]
