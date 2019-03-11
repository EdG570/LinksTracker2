var baseInit = (function () {
    var scorecardStatsCont = $('#cont');
    var headerHoleNum = $('#hole-num-header');
    var ajaxErrorAlert = $('.alert-danger');

    var init = function() {
        scorecardStatsCont.hide();
        headerHoleNum.hide();
        ajaxErrorAlert.hide();
    };

    return {
        init: init
    };
}());

var ui = (function () {
    var scorecardTbl = $('#scorecard-tbl');
    var headerHoleNum = $('#header-hole-number');

    var setHeaderHoleNum = function(holeNumber) {
        headerHoleNum.text(holeNumber);
    };

    var setHoleId = function(holeNum, holeIdList) {
        var targetId = null;

        for (var i = 0; i < holeIdList.length; i++) {
            if (holeIdList[i].Number === holeNum) {
                $('#holeId').text(holeIdList[i].Id);
                targetId = holeIdList[i].Id;
                break;
            }
        };

        return targetId;
    };

    var setScorecardHoleScore = function (holeId, score) {
        scorecardTbl.find('#' + holeId).find('.scorecard-score').text(score);
    };

    var setScorecardScoreTotal = function () {
        var total = 0;

        scorecardTbl.find('.scorecard-score').each(function () {
            if ($(this).text().length > 0 && $(this).text() !== '') {
                total += parseInt($(this).text());
            }
        });

        scorecardTbl.find('#total-score').text(total);
    }

    var buildScorecard = function(data) {
        var html = '';
        var holeIdList = [];
        var parTotal = 0;

        html += '<tr>';
        html += '<th class="scorecard-holes center">Hole</th>';
        html += '<th class="center">White</th>';
        html += '<th class="scorecard-pars center">Par</th>';
        html += '<th class="center">Score</th>';
        html += '</tr>';

        $.each(data.Holes, function (key, item) {
            html += '<tr id="' + item.Id + '">';
            html += '<td class="scorecard-holes center">' + item.Number + '</td>';
            html += '<td class="center">' + item.Yardage + '</td>';
            html += '<td class="scorecard-pars center">' + item.Par + '</td>';
            html += '<td class="scorecard-score center">' + '' + '</td>';
            html += '</tr>';

            parTotal += item.Par;

            holeIdList.push({
                Id: item.Id,
                Number: item.Number
            });
        });

        html += '<tr>';
        html += '<td class="hidden-td"></td>';
        html += '<td class="scorecard-total">Totals:</td>';
        html += '<td id="par-total" class="scorecard-total">' + parTotal + '</td>';
        html += '<td id="total-score" class="scorecard-total"></td>';
        html += '</tr>';

        scorecardTbl.html(html);
        return holeIdList;
    };

    var init = function() {

    };

    return {
        init: init,
        buildScorecard: buildScorecard,
        setHeaderHoleNum: setHeaderHoleNum,
        setHoleId: setHoleId,
        setScorecardHoleScore: setScorecardHoleScore,
        setScorecardScoreTotal: setScorecardScoreTotal
    };
}());

var courseSelect = (function () {
    var formCont = $('#form-cont');
    var form = formCont.find('#course-select-form');
    var input = form.find('#course-select');
    var submitBtn = form.find('button');
    var error = form.find('.error-msg');
    var selectedCourseId = null;

    var showError = function () {
        error.show();
    };

    var hideError = function () {
        error.hide();
    };

    var hideCourseSelect = function() {
        formCont.hide();
    };

    var getCourseId = function() {
        return selectedCourseId;
    }

    var validate = function() {
        if (input.val() === '' || input.val() === null) {
            showError();
            return false;
        } else {
            hideError();
            return true;
        }
    };

    form.on('submit', function (e) {
        e.preventDefault();
        if (!validate()) return false;
        selectedCourseId = input.val();
        hideCourseSelect();
        playFlow.init();
    });

    var init = function() {
        hideError();
    };

    return {
        init: init,
        getCourseId: getCourseId
    };
}());

var stats = (function() {
    var getNewDonutChart = function(vals) {
        return new Morris.Donut({
            element: 'stats-donut',
            data: [
                { label: "Double Bogeys", value: vals.doubleBogeys },
                { label: "Bogeys", value: vals.bogeys },
                { label: "Pars", value: vals.pars },
                { label: "Birdies", value: vals.birdies },
                { label: "Eagles", value: vals.eagles }
            ],
            colors: ['red', 'orange', 'green', 'lightblue', 'darkblue'],
            resize: true
        });
    };

    var init = function() {

    };

    return {
        init: init,
        getNewDonutChart: getNewDonutChart
    };
}());

var playFlow = (function () {
    var selectedCourseId = null;
    var course = null;
    var currentHoleNum = null;
    var currentHoleId = null;
    var holeIdList = [];
    var donutChartValues = {
        doubleBogeys: 0,
        bogeys: 0,
        pars: 0,
        birdies: 0,
        eagles: 0
    };
    var completedHolesData = [];

    var scorecardStatsCont = $('#cont');
    var headerHoleNum = $('#hole-num-header');
    var errorAlert = $('.alert-danger');
    var statsForm = $('#user-stats-form');

    var getCurrentHoleId = function() {
        return currentHoleId;
    };

    var resetStatsForm = function() {
        statsForm.find('.selected').removeClass('selected').css('background-color', '#777777');
        statsForm.find('#penalties').val('Penalties');
        statsForm.find('#putts').val('Putts');
        statsForm.find('#score').val('Score');
        statsForm.find('.error-msg').hide();
    };

    var getCourseSuccessCallback = function(data) {
        course = JSON.parse(data);
        holeIdList = ui.buildScorecard(course);
        ui.setHeaderHoleNum(currentHoleNum);
        currentHoleId = ui.setHoleId(currentHoleNum, holeIdList);
    };

    var ajaxGetCourseById = function() {
        $.ajax({
            url: "/Play/Course/" + selectedCourseId,
            data: selectedCourseId,
            type: "GET",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: getCourseSuccessCallback,
            error: function (errormessage) {
                errorAlert.show("fast", function() {
                    setTimeout(function() {
                        errorAlert.hide("slow");
                    }, 5000);
                });
            }
        });
    };

    var saveNewRecords = function(record) {
        completedHolesData.push(record);
    };

    var calculateDonutChartVals = function() {
        var doubleBogeys = 0;
        var bogeys = 0;
        var pars = 0;
        var birdies = 0;
        var eagles = 0;

        $.each(completedHolesData, function(index, item) {
            var diff = item.Score - item.Hole.Par;

            switch (diff) {
            case -2:
                eagles++;
                break;
            case -1:
                birdies++;
                break;
            case 0:
                pars++;
                break;
            case 1:
                bogeys++;
                break;
            case 2:
                doubleBogeys++;
                break;
            default:

            }
        });

        donutChartValues.doubleBogeys = doubleBogeys;
        donutChartValues.bogeys = bogeys;
        donutChartValues.pars = pars;
        donutChartValues.birdies = birdies;
        donutChartValues.eagles = eagles;
    };

    var createStatsSuccessCallback = function (data) {
        var record = $.parseJSON(data);
        currentHoleNum = record.Hole.Number + 1;
        ui.setHeaderHoleNum(currentHoleNum);
        ui.setScorecardHoleScore(record.HoleId, record.Score);
        currentHoleId = ui.setHoleId(currentHoleNum, holeIdList);
        ui.setScorecardScoreTotal();
        saveNewRecords(record);
        calculateDonutChartVals();
        $('#stats-donut').html('');
        stats.getNewDonutChart(donutChartValues);
        resetStatsForm();
    };

    var ajaxCreateStats = function(statsObj) {
        $.ajax({
            url: "/Play/Stats/",
            data: JSON.stringify(statsObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: createStatsSuccessCallback,
            error: function (errormessage) {
                errorAlert.show("fast", function () {
                    setTimeout(function () {
                        errorAlert.hide("slow");
                    }, 5000);
                });
            }
        });
    }

    statsForm.on('click', '.input-btn', function(e) {
        e.preventDefault();

        if ($(this).hasClass('selected')) {
            $(this).removeClass('selected').css('background-color', '#777777');
        } else {
            $(this).addClass('selected').css('background-color', '#8FB996');
        }
        
    });

    var validate = function() {
        var gir = $('#gir').hasClass('selected');
        var fir = $('#fir').hasClass('selected');
        var upanddown = $('#upanddown').hasClass('selected');
        var putts = $('#putts');
        var penalties = $('#penalties');
        var score = $('#score');

        if (putts.val() === '' || putts.val() === null) {
            putts.prev('.error-msg').show();
            return false;
        } else {
            putts.prev('.error-msg').hide();
        }

        if (penalties.val() === '' || penalties.val() === null) {
            penalties.prev('.error-msg').show();
            return false;
        } else {
            penalties.prev('.error-msg').hide();
        }

        if (score.val() === '' || score.val() === null) {
            score.prev('.error-msg').show();
            return false;
        } else {
            score.prev('.error-msg').hide();
        }

        return true;
    };

    var buildStatsObj = function(holeId) {
        return {
            GIR: $('#gir').hasClass('selected'),
            FIR: $('#fir').hasClass('selected'),
            UpAndDown: $('#upanddown').hasClass('selected'),
            Penalties: $('#penalties').val(),
            Putts: $('#putts').val(),
            Score: $('#score').val(),
            CreatedAt: null,
            UpdatedAt: null,
            HoleId: holeId,
            UserId: null
        };
    }

    statsForm.on('submit', function (e) {
        e.preventDefault();
        if (!validate()) return false;
        ajaxCreateStats(buildStatsObj(currentHoleId));
    });


    var init = function () {
        selectedCourseId = courseSelect.getCourseId();
        selectedCourseId = parseInt(selectedCourseId);
        ajaxGetCourseById();
        currentHoleNum = 1;
        statsForm.find('.error-msg').hide();
        scorecardStatsCont.show();
        headerHoleNum.show();
    };

    return {
        init: init
    }
}());

$(document).ready(function () {
    baseInit.init();
    courseSelect.init();
});