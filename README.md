# No360HUD
Disables 360 HUD for maps that have no valid rotation events
Before 360 update rotation events were unused so mappers used it.
This will fix maps like Osumemories to have normal HUD.
## How it works
It goes through all the beatmapEventData
and looks after rotation events (Event 14 and 15).
If it finds an event then it checks if it's a valid one.
If there are no valid events found then it sets spawnRotationEventsCount to be 0
so the game doesn't enable the 360 HUD elements!

Made by Rugtveit, updated by Mystogan.