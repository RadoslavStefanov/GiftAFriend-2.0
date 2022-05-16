function printToastr(input)
{
    let commands = input.split(':');

    for (let i = 0; i < commands.length; i++)
    {
        if (commands[i].length > 2)
        {
            switch (commands[i].split('-')[0])
            {
                case 'info': toastr.info(commands[i].split('-')[1]); break;
                case 'warning': toastr.warning(commands[i].split('-')[1]); break;
                case 'error': toastr.error(commands[i].split('-')[1]); break;
                case 'success': toastr.success(commands[i].split('-')[1]); break;
                default: toastr.error("Unknown toastrr command from server! Contact system admin!")
            }
        }
    }
}