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

- **Discord Notifications**: Notifications can be sent to Discord.
- **Scraping**: Successfully gathers all data from Sportsbet

- Only presents data for the first game in the list on sportsbet- data is gathered for all available games in the "Matches" list, although will need to be managed to be usable.
- Discord message currently just sends the data scraped from Sportsbet in a readable format to the linked webhook.
- No arbitrage calculations are being done at the current time
- Ladbrokes scraping is in progress