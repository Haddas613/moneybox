#! /bin/bash
#
# This script creates a zip folder output for uploading to HackerRank
#
# Requires the zip utility to be installed.
# sudo apt install zip

ZIP=${1:-moneybox-withdrawal.zip}

rm $ZIP
zip $ZIP -r . -i *.yml *.md *.sln *.csproj *.cs .gitignore
zip $ZIP -d '*/bin/*'
zip $ZIP -d '*/obj/*'
