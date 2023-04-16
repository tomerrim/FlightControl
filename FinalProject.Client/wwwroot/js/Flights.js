var connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5059/flightsHub").build();

connection.start().then(function () {
    console.log("Connected to SignalR hub");
}).catch(function (err) {
    console.error(err.toString());
});

connection.on("refreshLoggers", function (logger) {
    // Update table with new data 
    const tbody = document.querySelector("#flightsTableBody");

    //change the format to be the same as already displayed
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

    //creates new tr element and insert it on the top of the table
    const tr = document.createElement('tr');
    tr.innerHTML = `<td>${logger.leg?.id}</td>
                    <td>${logger.flight?.id}</td>
                    <td>${inDate}</td>
                    <td>${outDate}</td>`;
    tbody.insertAdjacentElement('afterbegin', tr);
});





