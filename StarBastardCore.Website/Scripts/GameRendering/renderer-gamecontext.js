function gameContext(gameboard) {
    $this = this;
    var $gameboard = gameboard;
    
    this.systems = function(){
        return $gameboard.Systems;
    };
    
    this.getSelectedSystem = function(){
        return $('body').data('activeSystem');
    };

    this.setSelectedSystem = function(system){
         $('body').data('activeSystem', system);
    };


}





