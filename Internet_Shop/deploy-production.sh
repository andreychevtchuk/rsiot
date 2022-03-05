#!/bin/bash

sudo dotnet publish --configuration Release -p:ASPNETCORE_ENVIRONMENT=Production -o:/var/www/Internet_Shop

sudo cp ./DeployFiles/default /etc/nginx/sites-available/

sudo cp ./DeployFiles/products-api.service /etc/systemd/system/

sudo systemctl enable products-api.service
sudo systemctl start products-api.service
sudo systemctl status products-api.service

