# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='product',
            name='productName',
            field=models.CharField(default=b'none', max_length=200),
            preserve_default=True,
        ),
        migrations.AlterField(
            model_name='product',
            name='link',
            field=models.CharField(max_length=200),
            preserve_default=True,
        ),
    ]
