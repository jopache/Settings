#!/bin/bash
#.NET Core
echo "********************"
echo "Installing dotnet"
echo "********************"
curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.gpg
sudo mv microsoft.gpg /etc/apt/trusted.gpg.d/microsoft.gpg
sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/microsoft-ubuntu-xenial-prod xenial main" > /etc/apt/sources.list.d/dotnetdev.list'
sudo apt-get update
sudo apt-get install -y dotnet-sdk-2.0.0

# Visual Studio Code
echo "********************"
echo "Installing vs code"
echo "********************"
sudo sh -c 'echo "deb [arch=amd64] https://packages.microsoft.com/repos/vscode stable main" > /etc/apt/sources.list.d/vscode.list'
sudo apt-get update
sudo apt-get install -y code

# Git
echo "********************"
echo "Installing git"
echo "********************"
sudo apt-get install -y git

# Node
echo "********************"
echo "Installing node"
echo "********************"
curl -sL https://deb.nodesource.com/setup_8.x | sudo -E bash -
sudo apt-get update
sudo apt-get install -y nodejs

# Google Chrome
echo "********************"
echo "Installing chrome"
echo "********************"
wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | sudo apt-key add - 
sudo sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google-chrome.list'
sudo apt-get update 
sudo apt-get install -y google-chrome-stable

echo "********************"
echo "Downloading Repo"
echo "********************"
cd ~/
mkdir -p proj/jopache
cd ~/proj/jopache
git clone https://github.com/jopache/Settings.git
cd ~/proj/jopache/Settings

# PostgreSql
# sudo apt-get install -y postgresql postgresql-contrib

#PostgreSQL - I think I dont have to add a user like this.  could be wrong
#sets up postgres with a role called settingsapp and a database called that as well
# sudo -u postgres createuser -d -l settingsapp
# sudo -u postgres createdb settingsapp
# creates a user, this requires some interventions
# sudo adduser settingsapp
# this connects you
# sudo -u settingsapp psql

# VBOX Additions fails that keep messing up my VM. Thanks snapshots
# vbox guest additions shenanigans
# sudo apt update && sudo apt dist-upgrade
# sudo apt install build-essential module-assistant dkms
# sudo m-a prepare
# sudo apt-get install -y linux-headers-generic
# sudo apt-get install linux-headers-`uname -r` dkms build-essential
# sudo apt-get install virtualbox-guest-dkms
# sudo apt-get install virtualbox-guest-dkms
# sudo apt-get install dkms build-essential virtualbox-guest-x11 linux-headers-generic linux-headers-virtual



