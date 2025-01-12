using Hippo.Application.Apps.Commands;
using Hippo.Application.Apps.Queries;
using Hippo.Application.Channels.Commands;
using Hippo.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Hippo.Web.Controllers;

public class AppController : WebUIControllerBase
{
    [HttpGet]
    public async Task<ActionResult<AppsVm>> Index()
    {
        AppsVm vm = await Mediator.Send(new GetAppsQuery());

        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid id)
    {
        try
        {
            AppDto dto = await Mediator.Send(new GetAppQuery { Id = id });

            return View(dto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult Edit(Guid id)
    {
        try
        {
            return View(new GetAppQuery { Id = id });
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UpdateAppCommand command)
    {
        try
        {
            await Mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpGet]
    public IActionResult New()
    {
        return View(new CreateAppCommand());
    }

    [HttpPost]
    public async Task<ActionResult<int>> New(CreateAppCommand command)
    {
        // TODO: handle validation errors
        var id = await Mediator.Send(command);
        return RedirectToAction(nameof(Details), new { id = id });
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            AppDto dto = await Mediator.Send(new GetAppQuery { Id = id });

            return View(dto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteAppCommand command)
    {
        try
        {
            await Mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }
}
