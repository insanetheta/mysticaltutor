# -*- coding: utf-8 -*-
from __future__ import unicode_literals

from django.db import models, migrations


class Migration(migrations.Migration):

    dependencies = [
        ('dealfinder', '0003_auto_20150223_2134'),
    ]

    operations = [
        migrations.CreateModel(
            name='CardSet',
            fields=[
                ('id', models.AutoField(verbose_name='ID', serialize=False, auto_created=True, primary_key=True)),
                ('setId', models.CharField(default=b'none', max_length=200)),
                ('name', models.CharField(default=b'none', max_length=200)),
                ('type', models.CharField(default=b'none', max_length=200)),
                ('block', models.CharField(default=b'none', max_length=200)),
                ('total', models.IntegerField(default=0)),
                ('cardIds', models.CharField(default=b'', max_length=500)),
            ],
            options={
            },
            bases=(models.Model,),
        ),
    ]
