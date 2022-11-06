$(document).ready(function () {
    Setup()
});

function handleError(error) {
    if (error) {
        console.error(error);
    }
}

function  Setup(){
    var sessionId = $('#sessionid').val()
    var apiKey = $('#apikey').val()
    var token = $('#token').val()
    
    var session = OT.initSession(apiKey, sessionId);

    // Subscribe to a newly created stream
    session.on('streamCreated', function streamCreated(event) {
        var subscriberOptions = {
            insertMode: 'append',
            width: '100%',
            height: '100%'
        };
        session.subscribe(event.stream, 'subscriber', subscriberOptions, handleError);
    });

    session.on('sessionDisconnected', function sessionDisconnected(event) {
        console.log('You were disconnected from the session.', event.reason);
    });

    // initialize the publisher
    var publisherOptions = {
        insertMode: 'append',
        width: '100%',
        height: '100%'
    };
    var publisher = OT.initPublisher('publisher', publisherOptions, handleError);

    // Connect to the session
    session.connect(token, function callback(error) {
        if (error) {
            handleError(error);
        } else {
            // If the connection is successful, publish the publisher to the session
            session.publish(publisher, handleError);
        }
    });
}
