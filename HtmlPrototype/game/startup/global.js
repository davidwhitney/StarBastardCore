function generateRandomNumber(maxValue){
    return Math.ceil(Math.random()*maxValue);
}

var gameboard = "";
var context = "";
var ui = "";

var $playerRenderer = new playerAreaRender();
var $worldRenderer = new gameWorldRenderer();
var $detailRenderer = new detailedViewRenderer();

function newGame(){
    var generator = new systemGenerator();
    gameboard = generator.generateSystems();

    context = new gameContext(gameboard, function(context){
        render(context);
    });

    window.gameboard = gameboard;
    window.context = context;

    $worldRenderer.render(context, $("#systems"));

    ui = new uiComponents(function(context){
        render(context);
    });

    ui.bindToUi();
    context.startTurn();
}

function render(context){
    $playerRenderer.render(context, context.currentPlayer());
    $detailRenderer.render(context, window.context.getSelectedSystem());
}