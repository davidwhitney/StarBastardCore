function gameContext(gameboard, updateUiFunction) {
    $this = this;
    var $gameboard = gameboard;
    var $currentRound = 0;
    var $currentPlayerTurn = 0;
    var $thisTurn = null;
    var $currentPlayer = null;
    var $updateUiFunction = updateUiFunction;

    var $players = new Array();
    $players[1] = new player(1, "Player 1");
    $players[2] = new player(2, "Player 2");

    this.systems = function(){
        return $gameboard;
    };

    this.currentRound = function(){
        return $currentRound;
    };
    this.currentPlayer = function(){
        return $currentPlayer;
    };

    this.currentPlayerTurn = function(){
      return $currentPlayerTurn;
    };

    this.thisTurn = function(){
      return $thisTurn;
    };

    this.startTurn = function(){
        //commit previous turn
        if($thisTurn != null){
            $thisTurn.commitTurn();
        }

        //start next turn
        if($currentPlayerTurn == 1){
            $currentPlayerTurn = 2;
        }
        else if($currentPlayerTurn == 2){
            $currentPlayerTurn = 1;
            $currentRound++;
        }

        if($currentPlayerTurn == 0){
            $currentPlayerTurn = 1;
            $currentRound = 1;
        }

        $currentPlayer = $players[$currentPlayerTurn];

        $thisTurn = new turn($currentPlayer);
        $thisTurn.tick(this);


        $(".round-number").text($currentRound);

        this.updateUi();
    };

    this.getSelectedSystem = function(){
        return $('body').data('activeSystem');
    };

    this.setSelectedSystem = function(system){
         $('body').data('activeSystem', system);
    };

    this.queueAction = function(action){
        $thisTurn.actionQueue().push(action);

        this.updateUi();
    };

    this.updateUi = function(){
        $updateUiFunction($this);
        //$playerRenderer.render($currentPlayer);
        //$worldRenderer.render($uiElement);
    };


}





