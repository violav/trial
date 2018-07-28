$(function () {
    $('#calendar').fullCalendar({
        now: '2018-04-07',
        editable: true, // enable draggable events
        aspectRatio: 1.8,
        scrollTime: '00:00', // undo default 6am scrollTime
        header: {
            left: 'today prev,next',
            center: 'title',
            right: 'timelineDay,timelineThreeDays,agendaWeek,month'
        },
        defaultView: 'timelineDay',
        views: {
            timelineThreeDays: {
                type: 'timeline',
                duration: { days: 3 }
            }
        },
        resourceLabelText: 'Rooms',
        //resources: [
        //    { id: 'a', title: 'Auditorium A' },
        //    { id: 'b', title: 'Auditorium B', eventColor: 'green' },
        //    { id: 'c', title: 'Auditorium C', eventColor: 'orange' },
        //    {
        //        id: 'd', title: 'Auditorium D', children: [
        //            { id: 'd1', title: 'Room D1' },
        //            { id: 'd2', title: 'Room D2' }
        //        ]
        //    },
        //    { id: 'e', title: 'Auditorium E' },
        //    { id: 'f', title: 'Auditorium F', eventColor: 'red' },
        //    { id: 'g', title: 'Auditorium G' },
        //    { id: 'h', title: 'Auditorium H' },
        //    { id: 'i', title: 'Auditorium I' },
        //    { id: 'j', title: 'Auditorium J' },
        //    { id: 'k', title: 'Auditorium K' },
        //    { id: 'l', title: 'Auditorium L' },
        //    { id: 'm', title: 'Auditorium M' },
        //    { id: 'n', title: 'Auditorium N' },
        //    { id: 'o', title: 'Auditorium O' },
        //    { id: 'p', title: 'Auditorium P' },
        //    { id: 'q', title: 'Auditorium Q' },
        //    { id: 'r', title: 'Auditorium R' },
        //    { id: 's', title: 'Auditorium S' },
        //    { id: 't', title: 'Auditorium T' },
        //    { id: 'u', title: 'Auditorium U' },
        //    { id: 'v', title: 'Auditorium V' },
        //    { id: 'w', title: 'Auditorium W' },
        //    { id: 'x', title: 'Auditorium X' },
        //    { id: 'y', title: 'Auditorium Y' },
        //    { id: 'z', title: 'Auditorium Z' }
        //],

        //events: [

        //    // background event, associated with a resource
        //    { id: 'bg1', resourceId: 'b', rendering: 'background', start: '2018-04-07T01:00:00', end: '2018-04-07T04:00:00' },

        //    // background event, NOT associated with a resource
        //    { id: 'bg2', rendering: 'background', start: '2018-04-07T05:00:00', end: '2018-04-07T08:00:00' },

        //    // normal events...
        //    { id: '1', resourceId: 'b', start: '2018-04-07T02:00:00', end: '2018-04-07T07:00:00', title: 'event 1' },
        //    { id: '2', resourceId: 'c', start: '2018-04-07T05:00:00', end: '2018-04-07T22:00:00', title: 'event 2' },
        //    { id: '3', resourceId: 'd', start: '2018-04-06', end: '2018-04-08', title: 'event 3' },
        //    { id: '4', resourceId: 'e', start: '2018-04-07T03:00:00', end: '2018-04-07T08:00:00', title: 'event 4' },
        //    { id: '5', resourceId: 'f', start: '2018-04-07T00:30:00', end: '2018-04-07T02:30:00', title: 'event 5' }




        /*
        : function (callback)
        {
            alert('resource');
            $.ajax({
                url: '/Home/GetRsouerceData',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var resources = [];

                    $.each(result, function (i, data) {
                        resources.push(
                            {
                                id: data.id,//'1',
                                 title: data.title
                            });
                    });

                    callback(resources);
                }
            });
        }
        */


        resources: function (callback) {
            var resources = [];
            var path = window.location.href.substr(0, window.location.href.lastIndexOf(window.location.pathname)) + '/Content/files/Risorse.json';
            fetch(path)//('..\Content%5Cfiles%5CRisorse.json') //('http://localhost:3285/Content/files/Risorse.json') 
                .then(function (response) {
                    if (response.status !== 200) {
                        console.warn('Looks like there was a problem. Status Code: ' +
                            response.status);
                        return;
                    }

                    response.json().then(function (data) {
                        let option;
                        for (let i = 0; i < data.length; i++) {
                            resources.push(
                                {
                                    id: data[i].id,
                                    title: data[i].title,
                                    eventColor: data[i].eventColor
                                }
                            )
                        }
                        callback(resources);
                    }
                    );
                }).catch(function (eccezione) { alert('eccezione in resource -> ' + eccezione.message); });
        },
        //   $.getJSON("C:\Users\Viola\Downloads\calendar\MVC5FullCalandarPlugin - Copia - Copia\MVC5FullCalandarPlugin\Content\files\Risorse.json", function (json) { alert('resources -> json' + json); }),



        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '/Home/GetCalendarData',
                type: "GET",
                dataType: "JSON",

                success: function (result) {
                    var events = [];

                    $.each(result, function (i, data) {
                        // alert('inizio -> ' + moment(data.Start_Date).format('YYYY-MM-DD HH:mm'));
                        events.push(
                            {
                                id: data.Id,//'1',
                                resourceId: data.ResourceId,//'b',
                                title: data.Title,
                                description: data.Desc,
                                start: moment(data.Start_Date).format('YYYY-MM-DD HH:mm'),
                                end: moment(data.End_Date).format('YYYY-MM-DD HH:mm'),
                                nota: data.nota
                                //backgroundColor: data.Colore,
                                //borderColor: data.Colore// "#fc0101"
                            });
                    });
                    callback(events);
                }
            });
        },
        //],


        dayClick: function (date, jsEvent, view, resourceObj) {
            try {

                var url = '/Home/AddDate';
                var resObj = resourceObj == undefined ? '0' : resourceObj.id;

                $.ajax({
                    type: "GET",
                    url: url,
                    contentType: "application/json; charset=utf-8",
                    data: "userId=" + resObj + "&dt=" + date.format(),
                    datatype: "json",
                    success: function (data) {
                        $("#dialog").dialog({
                            modal: true,
                            title: "SCHEDULAZIONE",
                            open: function () {
                                $(this).html(data)
                            }
                        });

                    },
                    error: function () {
                        alert("Dynamic content load failed.");
                    }
                });
            }
            catch (ecc) { alert('errore in dayClick ->' + ecc.message); }
        },


        //function() {
        //     $(".popupLinks").click(function (e) {
        //         var url = this.href;
        //         var dialog = $("#dialog");
        //         if ($("#dialog").length == 0) {
        //             dialog = $('<div id="dialog" style="display:hidden"></div>').appendTo('body');
        //         }
        //         dialog.load(
        //             url,
        //            // {}, // omit this param object to issue a GET request instead a POST request, otherwise you may provide post parameters within the object
        //             function (responseText, textStatus, XMLHttpRequest) {
        //                 dialog.dialog({
        //                     close: function (event, ui) {
        //                         dialog.remove();
        //                     },
        //                     modal: true,
        //                     width: 460, resizable: false
        //                 });
        //             }
        //         );
        //         return false;
        //     });
        // },

        eventClick: function (calEvent, jsEvent, view) {

            alert('Event: ' + calEvent.title);
            alert('Coordinates: ' + jsEvent.pageX + ',' + jsEvent.pageY);
            alert('View: ' + view.name);

            // change the border color just for fun
            $(this).css('border-color', 'red');

            //alert(new Date(2018, 11, 10));
            alert('calEvent.date -> ' + calEvent.start);
            alert('calEvent.date -> ' + calEvent.end);

            alert('calEvent.start -> ' + moment(calEvent.start).format('DD/MM/YYYY h:m'));
            alert('calEvent.end -> ' + moment(calEvent.end).format('YYYY-MM-DD'));
            alert('calEvent.id-> ' + calEvent.id);


            var url = '/Home/ModifyEvent';
            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json; charset=utf-8",
                data: "evtId=" + calEvent.id,
                datatype: "json",
                success: function (data) {
                    $("#dialog").dialog({
                        modal: true,
                        title: "SCHEDULAZIONE",
                        open: function () {
                            $(this).html(data)
                        }//,
                        //buttons: {
                        //    Ok: function () {
                        //        $(this).dialog("submit");
                        //    }
                        //}
                    });

                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });

        }

    })
})

    //#calendar cellEls .fc-widget-content.fc-sun.fc-other-month.fc-past
    //$(".fc-day").click(function () {
    //    alert('did it');
    //    alert($(".fc-day").innerHTML)
    //    alert($(".fc-day").text)
    //    alert($(".cellEls").innerHTML)
    //    alert($(".cellEls").text)
    //})



