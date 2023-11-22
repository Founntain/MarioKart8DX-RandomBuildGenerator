# Mario Kart 8 Deluxe Random Build Generator

This discord bot creates a random build containing a set of 1 character, 1 body, 1 tire and 1 glider and also displays the stats of this build.  
It's also possible to create multiple builds at the same time, up to 12. 
> Please keep in mind that statistics only get rendered for 1 build and **not** for multiple

## Sounds cool. How can I add this bot to my server?
Glad that you ask 😄  
If you want to add the bot to your discord server you can [use this link](https://discord.com/api/oauth2/authorize?client_id=836318982080167946&permissions=19456&scope=bot%20applications.commands) . Please check if you have enough permissions if you want to add the bot to the server, otherwise kindly ask someone to add it for you.  

Have fun 😃
## Commands
`/info`: Prints info about the bot and commands  
`/support`: Gives you all possible ways how to contact me for help.  
`/gen-build`: Generates up to 12 builds for your selected game version. *Pro Tip: You can also exclude inline-bikes if you don't like them 🙃*

## Building the bot by yourself
If, for whatever reason you want to build the bot by yourself, you can do so by pulling this repository and configuring it with your bot token. Your bot token can be found in the [Discord Developer Portal](https://discord.com/developers/applications) under your bot's name, select the "Bot" option under settings and then you should be able to see it underneath the bots username.

If you want to build a brand new bot, you'll need to give the bot the "Bot" and "Applications.Commands" permissions under scope.
Under "Bot Permissions", you'll need to enable "Read Messages/View Channels", "Send Messages", and "Embed Links".

Once thats finished, go into your code and create a config.json file inside the root folder of the project and fill it with the following:
```JSON
{
  "Token": "<your-bot-token-here>"
}
```

If you want to test the bot on a specific server, you will need to replace the default GUID with your discord server ID that you want to test it on. This can be found when you right click on the server icon on left server menu. *(You will need to have [Developer Mode enabled](https://support.discord.com/hc/en-us/articles/206346498-Where-can-I-find-my-User-Server-Message-ID-) for you to be able to see this id.)* 

You can find the default GUID on line 11 in the "CommandRegisterer" class in the "Classes" folder.

### Special Thanks
- **SourRaindrop** for making some of the assets and helping in general
- **Luigi_Fan2** for making an awesome chart of all the stats in the game
- **u/Deafboy91** for making an awesome Online Builder for MK8DX and using it as insperation for the bar designs

### Contibution
If you want to contribute to this project, you can do so by opening a pull request.
## Screenshots
###  Simple solo build
![](https://cdn.discordapp.com/attachments/419319912104984577/1084130243800997918/build.png)  
### Example of mulitple builds
![](https://cdn.discordapp.com/attachments/419319912104984577/1084130543920234618/build.png)
