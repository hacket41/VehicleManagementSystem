using backend.Data.DTO.Request;
using backend.Data.Entities;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class FinancialReportController()
{
    
}