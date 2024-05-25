﻿namespace Weather.Contracts;

public record Weather(
    int? Id,
    string Main,
    string Description,
    string Icon
);
