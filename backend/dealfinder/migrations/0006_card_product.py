# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0005_card'),
    ]

    operations = [
        migrations.AddField(
            model_name='card',
            name='product',
            field=models.OneToOneField(null=True, blank=True, to='dealfinder.Product'),
            preserve_default=True,
        ),
    ]
