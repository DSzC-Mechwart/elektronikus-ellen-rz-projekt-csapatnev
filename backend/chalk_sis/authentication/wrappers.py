from functools import wraps
from django.http import JsonResponse
from api.models import UserData


def require_role(role_list):
    def decorator(view_func):
        @wraps(view_func)
        def _wrapped_view(request, *args, **kwargs):
            user_data = UserData.objects.get(for_user=request.user)
            if user_data.role not in role_list:
                return JsonResponse({"error": "You do not have permission"}, status=403)
            return view_func(request, *args, **kwargs)
        return _wrapped_view
    return decorator
