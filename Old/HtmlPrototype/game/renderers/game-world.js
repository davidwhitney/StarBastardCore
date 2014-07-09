function gameWorldRenderer() {


    this.render = function(context, target){

        target.html('');

        var $gameboard = context.systems();
        var $boxHeight = 42;
        var $boxWidth = 42;

        var systemOffset = 1;
        var currentLine = $("<div style='clear: both;'></div>");
        var boxesOnThisLine = 0;

        var metaData = new Array();

        $.each($gameboard, function(key, value) {
            var system = $gameboard[key];

            metaData.push(new renderedSystemMetaData(key, system.systemNumber()))
            var div = '<div class="system" id="system-' + system.systemNumber() + '" style="width: '+$boxWidth+'px; height: '+$boxHeight+'px"></div>';
            currentLine.append(div);
            boxesOnThisLine++;

            if(systemOffset == 4 || (systemOffset - 37) == 4
                || systemOffset == 9 || (systemOffset - 37) == 9
                || systemOffset == 15 || (systemOffset - 37) == 15
                || systemOffset == 22 || (systemOffset - 37) == 22
                || systemOffset == 28 || (systemOffset - 37) == 28
                || systemOffset == 33 || (systemOffset - 37) == 33
                || systemOffset == 37 || (systemOffset - 37) == 37
                )
            {
                var pxOffset = 0;
                if(boxesOnThisLine == 4)
                {
                    pxOffset = $boxWidth * 1.5;
                }
                if(boxesOnThisLine == 5)
                {
                    pxOffset = $boxWidth * 1;
                }
                if(boxesOnThisLine == 6)
                {
                    pxOffset = $boxWidth * 0.5;
                }

                currentLine.css('padding-left', pxOffset);
                target.append(currentLine);

                currentLine = $("<div style='clear: both;'></div>");
                boxesOnThisLine = 0;
            }

            systemOffset++;
        });

        $.each(metaData, function(key, value) {
           $("#system-"+value.index()).data('id', value.id());
           $("#system-"+value.index()).data('index', value.index());
        });
    };

}

function renderedSystemMetaData(id, index){
    var $id=id;
    var $index=index;

    this.id = function(){
        return $id;
    }
    this.index = function(){
        return $index;
    }
}