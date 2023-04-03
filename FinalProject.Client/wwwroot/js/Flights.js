var connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5059/flightsHub")
    .build();

connection.start().then(function () {
    console.log("Connected to SignalR hub");
}).catch(function (err) {
    console.error(err.toString());
});

connection.on("addNewRow", function (logger) {
    console.log('addNewRow event received', logger);
    // Add new row to table here
    //const tr = document.createElement('tr');
    //document.getElementById("flightsTableBody").appendChild(tr);

});

connection.on("refreshLoggers", function (logger) {
    console.log('refreshLoggers event received', logger);
    // Update table with new data here
    const tbody = document.querySelector("#flightsTableBody");
    //tbody.innerHTML = "";

    const inDate = new Date(`${logger.in}`).toLocaleString("en-GB", {
        day: "numeric",
        month: "numeric",
        year: "numeric",
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
        hour12: false,
    });
    

    const outDate = new Date(`${logger.out}`).toLocaleString("en-GB", {
        day: "numeric",
        month: "numeric",
        year: "numeric",
        hour: "numeric",
        minute: "numeric",
        second: "numeric",
        hour12: false,
    });
   
    
    const tr = document.createElement('tr');
    tr.innerHTML = `<td>${logger.leg?.id}</td>
                    <td>${logger.flight?.id}</td>
                    <td>${inDate}</td>
                    <td>${outDate}</td>`;
    tbody.appendChild(tr);

    //loggers.forEach(logger => {
    //    const tr = document.createElement('tr');
    //    tr.innerHTML = "<td>" + logger.Leg.Id + "</td><td>" + logger.Flight.Id + "</td><td>" + logger.In + "</td><td>" + logger.Out + "</td>";
    //    tbody.appendChild(tr);
    //});
});





