# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0002_auto_20150223_2124'),
    ]

    operations = [
        migrations.AlterField(
            model_name='product',
            name='link',
            field=models.TextField(max_length=200),
            preserve_default=True,
        ),
    ]
