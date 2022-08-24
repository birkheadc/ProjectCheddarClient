# Todo

## Write a better readme, including what this game is planned to be

## Refactor client / tcp / packet classes
- Rename things like SendData(Packet packet) to SendPacket(Packet packet)
- Refactor Packet class, change things like Length() to just Length using a property with a custom getter.

## Design chunk distrubution method
### This may or may not be a good way to do this, but my current plan is to...
- Client should update server with it's current position, by telling the server when the player enters a new chunk.
- Server should then send information on chunks within CHUNK_VIEW_DISTANCE of that chunk.
- Client should destroy chunks that are no longer in range of this new chunk, because... 
- Server should only send updates to clients within range of their current chunk. I.e. once a player leaves range of (Chunk 0,0), the server will stop providing that client with any information on what goes on in that chunk. So the client should abandon that chunk, since it will essentially freeze.