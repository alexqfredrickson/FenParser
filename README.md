# FenParser
<b>FenParser</b> is a C# class library for .NET 8.0 that parses [Forsyth-Edwards notated records](https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation).

### Basic usage

First, construct a `FenParser` object and feed it a FEN string.

```csharp
FenParser parser = new FenParser("5k2/ppp5/4P3/3R3p/6P1/1K2Nr2/PP3P2/8 b - - 1 32");
```

Now FenParser's BoardStateData field exposes all of the necessary metadata used to describe the game's state, including:

* The active player color
* The en-passant square (if eligible)
* Piece placement and ranks
* Each player's kingside/queenside castling availability
* The game's halfmove counter
* The game's turn number

```csharp
bool castlingAvailability = parser.BoardStateData.WhiteCanQueensideCastle; // false
```
    
Additionally, the `BoardStateData.ToConsole()` method returns a command-line visualization of the board state. 

    -----------------
    | | | | | |k| | |
    -----------------
    |p|p|p| | | | | |
    -----------------
    | | | | |P| | | |
    -----------------
    | | | |R| | | |p|
    -----------------
    | | | | | | |P| |
    -----------------
    | |K| | |N|r| | |
    -----------------
    |P|P| | | |P| | |
    -----------------
    | | | | | | | | |
    -----------------
    
    The active player is Black.
    White cannot castle, and Black cannot castle.
    There are no squares eligible for en-passant capture.
    There has been 1 halfmove since the last pawn advance or capture.
    This is move number 32.

