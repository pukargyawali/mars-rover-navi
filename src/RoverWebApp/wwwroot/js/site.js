var myGamePiece;
var directionData;
var directionDataArray;
var dataPointCount = 0;
var roverColors = ["green", "blue", "red", "yellow", "black", "purple"];

function plotGraph() {
    drawGrid.plot();
}
var drawGrid = {
    canvas: document.getElementById("roverCanvas"),
    plot: function () {
        var w = 1000;
        var h = 500;
        this.context = this.canvas.getContext('2d');
        this.canvas.width = w;
        this.canvas.height = h;
        for (x = 0; x <= w; x += 100) {
            for (y = 0; y <= h; y += 100) {
                this.context.moveTo(x, 0);
                this.context.lineTo(x, h);
                this.context.stroke();
                this.context.moveTo(0, y);
                this.context.lineTo(w, y);
                this.context.stroke();
            }
        }
        //document.body.insertBefore(this.canvas, document.body.childNodes[6]);
    }
};

function updateGameArea(callback) {
    myGamePiece.newPos();
    myGamePiece.update();
    switch (myGamePiece.direction) {
        case "U":
            moveup();
            if (myGamePiece.y <= myGamePiece.destination[1]) {
                clearmove();
                myGamePiece.direction = 'S';
                directionData.newDirection();
                dataPointCount += 1
                callback()
            }
            break;
        case "L":
            moveleft();
            if (myGamePiece.x == myGamePiece.destination[0]) {
                clearmove();
                myGamePiece.direction = 'S';
                directionData.newDirection();
                dataPointCount += 1
                callback()
            }
            break;
        case "D":
            movedown();
            if (myGamePiece.y == myGamePiece.destination[1]) {
                clearmove();
                myGamePiece.direction = 'S';
                directionData.newDirection();
                dataPointCount += 1
                callback()
            }
            break;
        default:
            clearmove();
            break;
    }

}
function moveRoverAsync() {
    doNextPromise(0);
}

function processorPromise(array) {
    return new Promise((resolve) => {
        dataPointCount = 0;
        directionData = array;
        let directionDataLength = directionData.dataPoints.length;
        let roverColor = roverColors[Math.floor((Math.random() * 5))];
        myGamePiece = new component(15, 15, roverColor, directionData.initialAxis.startingX, directionData.initialAxis.startingY);
        directionData.newDirection();
        setInterval(updateGameArea, 20, function () {
            if (dataPointCount === directionDataLength) {
                resolve();
            }
        });
    });

}
function doNextPromise(d) {
    var array = directionDataArray;
    processorPromise(array[d])
        .then(x => {
            d++;
            if (d <= array.length)
                doNextPromise(d)
            else
                console.log("finished");
        })
}

function moveRoverAsyncOld() {
    var array = directionDataArray;
    for (var i = 0; i < array.length; i++) {
        //await new Promise(resolve => moveRover(array[i], resolve));
        console.log(i);
        moveRover(array[i])
    }
}

function moveRover(nextDirectionData) {
    directionData = nextDirectionData;
    myGamePiece = new component(15, 15, "green", directionData.initialAxis.startingX, directionData.initialAxis.startingY);
    directionData.newDirection();
    setInterval(updateGameArea(), 20)
}

function component(width, height, color, x, y) {
    this.width = width;
    this.height = height;
    this.speedX = 0;
    this.speedY = 0;
    this.x = x;
    this.y = y;
    this.direction = 'S'
    this.destination = [];
    this.update = function () {
        ctx = drawGrid.context;
        ctx.fillStyle = color;
        ctx.fillRect(this.x, this.y, this.width, this.height);
    }
    this.newPos = function () {
        this.x += this.speedX;
        this.y += this.speedY;

    }
}
function apiData(dataPoints, initialAxis) {
    this.dataPoints = dataPoints;
    this.initialAxis = initialAxis;
    this.newDirection = function () {
        var nextDataPoint = this.dataPoints.shift()
        if (nextDataPoint != undefined) {
            myGamePiece.direction = nextDataPoint.Direction;
            myGamePiece.destination = nextDataPoint.axis;
        }

    }
}
function moveup() {
    myGamePiece.speedY = -1;
}

function movedown() {
    myGamePiece.speedY = 1;
}

function moveleft() {
    myGamePiece.speedX = -1;
}

function moveright() {
    myGamePiece.speedX = 1;
}

function clearmove() {
    myGamePiece.speedX = 0;
    myGamePiece.speedY = 0;
    myGamePiece.direction = 'S';
}

$(document).ready(function () {
    plotGraph();
    $('#start-rover-btn').attr("disabled", "true");
    $('#customFileInputBtn').click(function () {
        var fileUpload = $('#customFileInput');
        if (fileUpload != null) {
            var files = fileUpload.get(0).files;
            if (files.length > 0) {
                UploadFile(files[0]);
            }
        }
        else {
            //write some error to the web page
        }

    });
    $('input[type=file]').change(function () {
        $('.error-msg').addClass('hidden');
        $('.check-icon').addClass('hidden');
        var val = $(this).val().toLowerCase(),
            regex = new RegExp("(.*?)\.(csv)$");        
        var fileName = val.substring(val.lastIndexOf("\\") + 1, val.length);
        $('.custom-file-label').html(fileName);
        if (!(regex.test(val))) {
            $(this).val('');
            $('.custom-file-label').html("Select File");
            $('.error-msg').removeClass('hidden')
        }
        else {
            $('.check-icon').removeClass('hidden')
        }
    });
});

function UploadFile(data) {
    var FD = new FormData();
    FD.append('roverControlFile', data);
    $.ajax({
        type: "POST",
        url: '/Rover/UploadControlFile/',
        contentType: false,
        processData: false,
        data: FD,
        type: 'POST',
        success: function (rovers) {            
            let apiDataArray = [];
            $('#start-rover-btn').removeAttr("disabled");
            rovers.forEach(rover => {
                var initialAxis = {
                    "startingX": rover.startingX,
                    "startingY": rover.startingY
                }
                var dataPoints = [];
                rover.graphNodes.forEach(node => {
                    let dataPoint = {
                        "axis": [node.xOffset, node.yOffset],
                        "Direction": node.name
                    }
                    dataPoints.push(dataPoint);
                });
                apiDataArray.push(new apiData(dataPoints, initialAxis));
            });
            directionDataArray = apiDataArray;
            $('.error-mssg-api').addClass('hidden');
            
        },
        error: function (jqXHR, status, err) {
            $('.error-mssg-api').removeClass('hidden');
        },
        complete: function () {
        }
    });
}