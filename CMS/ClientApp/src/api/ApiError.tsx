export default class extends Error
{
    public status: number;

    public constructor(status: number) {
        super(`API returned error code {status}.`);
        this.name = "ApiError";
        this.status = status;
    }
}
