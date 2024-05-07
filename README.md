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
- **Scraping**: Successfully gathers all data from Sportsbet and Ladbrokes
- **Detection**: Working between Sportsbet and Labrokes *only*, for the first game in the list on the webpage for AFL H2H.

- No Discord message is sent if no Aritrage is found: A console message is sent.
- Discord message is sent if an Arbitrage is found, including the teams playing, game time and date, prices for the odds, and bookmakers providing the prices.
- Including more bookmakers will be added in future.
- Currently working on being able to check multiple games at once- currently only checks the first, this is an issue if there are multiple games on in one day or within a short span of eachother.