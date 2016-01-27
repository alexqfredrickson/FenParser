# FenParser
<b>FenParser</b> is a simple C# library for use in parsing [Forsyth-Edwards notated records](https://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation).

### Basic usage

Just input a FEN string to extract metadata about a board's state.

    FenString fen = new FenString("5k2/ppp5/4P3/3R3p/6P1/1K2Nr2/PP3P2/8 b - - 1 32");
    FenParser fenParser = new FenParser(fen);
    BoardState board = fenParser.Parse();

Additionally, the `BoardState.ToConsole()` method returns a command-line visualization of the board state. 

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

