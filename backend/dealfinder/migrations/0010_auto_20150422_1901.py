# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0009_auto_20150421_0042'),
    ]

    operations = [
        migrations.AlterField(
            model_name='cardset',
            name='setCode',
            field=models.CharField(default=b'none', unique=True, max_length=200),
            preserve_default=True,
        ),
    ]
