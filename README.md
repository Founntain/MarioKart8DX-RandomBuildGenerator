# Mario Kart 8 Deluxe Random Build Generator

This discord bot creates a random build containing a set of 1 character, 1 body, 1 tire and 1 glider and also displays the stats of this build.  
It's also possible to create up to 12 builds at the same time. 

> [!NOTE]\
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

Once you have the token, create a config.json file inside the root folder of the project and fill it with the following:
```JSON
{
  "Token": "<your-bot-token-here>"
}
```
> [!IMPORTANT]\
> In order to test the bot locally, you'll need to add it to a test server. To do that, you'll need to generate a OAuth2 URL. After selecting the bot you want to add from the [Discord Developer Portal](https://discord.com/developers/applications), click on "OAuth2", then click "URL Generator". Next, you'll need to give the bot the "Bot" and "Applications.Commands" permissions under scope. Under "Bot Permissions", you'll need to select "Read Messages/View Channels", "Send > Messages", and "Embed Links". At the very bottom, there will be a link that you can navigate to that will let you choose the test server you want the bot to join.

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
