﻿namespace Core.ImageStorage.Repositories;
public interface IFilesRepository
{
    Task<int> Insert(string partition, string fileName);
}
