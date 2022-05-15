function drawNotifications(input)
{
    let notificationNodesArray = input.split('-');

    for (let i = 0; i < notificationNodesArray.length; i++)
    {
        let node = document.getElementById(notificationNodesArray[i])
        if (node != undefined)
        { node.style.display="block" }
    }

}