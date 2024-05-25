# Sports Betting Arbitrage Notification System

## Overview

This project automates the detection of arbitrage opportunities in sports betting webpages from Australia by scraping data from various websites, analyzing it for profitable bets, and notifying users via a Discord webhook when such opportunities are found.
- The project is currently only focusing on AFL matches from a short list of sites, this results in it not being overly effective, although this is a WIP
- The sites include: Sportsbet, Ladbrokes, NAB.

## Goals

- **Data Collection**: Scrape odds and event data from sports betting sites.
- **Arbitrage Detection**: Identify arbitrage opportunities for guaranteed profits.
- **Notifications**: Send alerts to Discord when arbitrage bets are available.

## Current Status/Recent Changes

**Major Update**

- **Project Converted to Discord Bot** - The project has now been converted to use a Discord App, instead of a webhook. This change was made due to the far greater utility and usability of a bot, along with the potential for further features

- **Discord Notifications**: Notifications can be sent to Discord.
- **Scraping**: Successfully gathers all data from Sportsbet and Ladbrokes
- **Detection**: Working between Sportsbet and Labrokes *only*, for the first game in the list on the webpage for AFL H2H.

**Progress Status**

- When /checkarbitrage is used, a message will be returned with the result
- Discord message is sent if an Arbitrage is found, including the teams playing, game time and date, prices for the odds, and bookmakers providing the prices.
- Including more bookmakers will be added in future.
- Currently working on being able to check multiple games from the websites at once- currently only checks the first, this is an issue if there are multiple games on in one day or within a short span of eachother.

## How do I set up my App?

**These steps will assume you have already created a bot in the Developer Portal**

*Required bot perms*
- Create commands

**Steps**
- Open config/config.json
- Enter your bot token where it says 'token-here'
- Copy this file
- Build the program
- Paste this file in bin/Debug/net6.0
- Run the program