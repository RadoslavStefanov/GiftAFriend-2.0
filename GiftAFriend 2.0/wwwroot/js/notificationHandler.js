function drawNotifications(input)
{
    let notificationNodesArray = input.split('-');

    let notificationNode = document.createElement('span');
    notificationNode.classList.add("badge");
    notificationNode.classList.add("badge-info");
    notificationNode.classList.add("right");
    notificationNode.innerText = "!";

    for (let i = 0; i < notificationNodesArray.length; i++)
    {
        let node = document.getElementById(notificationNodesArray[i])
        if (node != undefined)
        { node.appendChild(notificationNode) }
    }

}