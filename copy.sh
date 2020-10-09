#!/bin/bash

echo "ArgumentMarshaler Downloader"
echo "----------------------------"
echo "Current Directory:"
echo "${PWD}"
echo "----------------------------"

echo "BooleanMarshalerLib"
read -p "Download [Y/N]? " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]
then
    wget -q "https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.BooleanMarshalerLib.dll"
fi

echo "IntegerMarshalerLib"
read -p "Download [Y/N]? " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]
then
    wget -q "https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.IntegerMarshalerLib.dll"
fi

echo "StringMarshalerLib"
read -p "Download [Y/N]? " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]
then
    wget -q "https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.StringMarshalerLib.dll"
fi

echo "DoubleMarshalerLib"
read -p "Download [Y/N]? " -n 1 -r
echo ""
if [[ $REPLY =~ ^[Yy]$ ]]
then
    wget -q "https://github.com/sunriax/argument/releases/latest/download/RaGae.ArgumentLib.DoubleMarshalerLib.dll"
fi

echo "End of downloading"
