using Microsoft.EntityFrameworkCore;

class InstrumentEndpoint
{
    public void Configure(RouteGroupBuilder router)
    {
        router.MapGet("/", GetInstrumemnts);
        router.MapPost("/", CreateInstrument);
    }

    Task<List<Instrument>> GetInstrumemnts(SomethingDbCtx db)
    {
        // return db.Instruments.ToListAsync();

        return db.Instruments.Include(t => t.Squid).Select(t => new Instrument
        {
            Id = t.Id,
            Name = t.Name,
            Type = t.Type,
            SquidId = t.SquidId,
            Squid = new Squid
            {
                Id = t.Squid.Id,
                Name = t.Squid.Name,
            }
        }).ToListAsync();
    }
    async Task<IResult> CreateInstrument(SomethingDbCtx db, Instrument instrument)
    {
        db.Instruments.Add(instrument);
        await db.SaveChangesAsync();
        return Results.Created($"/api/instruments/{instrument.Id}", instrument);
    }

}
