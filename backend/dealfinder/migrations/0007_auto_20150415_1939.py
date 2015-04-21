# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0006_card_product'),
    ]

    operations = [
        migrations.RenameField(
            model_name='cardset',
            old_name='setId',
            new_name='setCode',
        ),
        migrations.AddField(
            model_name='cardset',
            name='gathererCode',
            field=models.CharField(default=b'none', max_length=200),
            preserve_default=True,
        ),
    ]
